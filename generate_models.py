#!/usr/bin/env python3
"""
Скрипт для генерации случайных пользователей-моделей с фотографиями
"""

import requests
import random
import string
import io
from PIL import Image, ImageDraw, ImageFont
import time

API_URL = "http://localhost:8080"

# Список случайных имён
FIRST_NAMES_FEMALE = [
    "Анна", "Мария", "Елена", "Ольга", "Наталья", "Татьяна", "Ирина",
    "Екатерина", "Светлана", "Юлия", "Дарья", "Алина", "Виктория",
    "Кристина", "Полина", "Александра", "София", "Анастасия", "Валерия"
]

FIRST_NAMES_MALE = [
    "Александр", "Дмитрий", "Сергей", "Андрей", "Алексей", "Максим",
    "Иван", "Артём", "Владимир", "Михаил", "Николай", "Павел",
    "Роман", "Денис", "Егор", "Константин", "Кирилл", "Олег"
]

LAST_NAMES = [
    "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов",
    "Васильев", "Соколов", "Михайлов", "Новиков", "Фёдоров", "Морозов",
    "Волков", "Алексеев", "Лебедев", "Семёнов", "Егоров", "Павлов",
    "Козлов", "Степанов"
]

def generate_random_image(name, sex_type):
    """Генерирует случайное изображение с именем"""
    # Создаём изображение 400x600
    width, height = 400, 600

    # Случайный цвет фона (пастельные тона)
    if sex_type == "Female":
        bg_colors = [
            (255, 192, 203),  # Pink
            (221, 160, 221),  # Plum
            (230, 230, 250),  # Lavender
            (255, 218, 185),  # Peach
            (255, 228, 225),  # Misty Rose
        ]
    else:
        bg_colors = [
            (173, 216, 230),  # Light Blue
            (176, 196, 222),  # Light Steel Blue
            (211, 211, 211),  # Light Gray
            (220, 220, 220),  # Gainsboro
            (245, 245, 220),  # Beige
        ]

    bg_color = random.choice(bg_colors)

    # Создаём изображение
    img = Image.new('RGB', (width, height), color=bg_color)
    draw = ImageDraw.Draw(img)

    # Рисуем простой силуэт (круг для головы)
    head_radius = 80
    head_center = (width // 2, height // 3)

    # Тень
    draw.ellipse(
        [head_center[0] - head_radius + 5, head_center[1] - head_radius + 5,
         head_center[0] + head_radius + 5, head_center[1] + head_radius + 5],
        fill=(150, 150, 150, 50)
    )

    # Голова
    draw.ellipse(
        [head_center[0] - head_radius, head_center[1] - head_radius,
         head_center[0] + head_radius, head_center[1] + head_radius],
        fill=(255, 220, 177)
    )

    # Плечи (прямоугольник)
    shoulders_top = head_center[1] + head_radius - 20
    shoulders_width = 160
    shoulders_height = 200
    draw.rectangle(
        [width // 2 - shoulders_width // 2, shoulders_top,
         width // 2 + shoulders_width // 2, shoulders_top + shoulders_height],
        fill=(100, 100, 150)
    )

    # Добавляем имя
    try:
        font = ImageFont.truetype("/System/Library/Fonts/Helvetica.ttc", 36)
    except:
        font = ImageFont.load_default()

    # Текст с именем
    text = name
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    text_position = ((width - text_width) // 2, height - 80)

    # Тень текста
    draw.text((text_position[0] + 2, text_position[1] + 2), text, fill=(100, 100, 100), font=font)
    # Основной текст
    draw.text(text_position, text, fill=(50, 50, 50), font=font)

    # Сохраняем в BytesIO
    img_byte_arr = io.BytesIO()
    img.save(img_byte_arr, format='JPEG', quality=85)
    img_byte_arr.seek(0)

    return img_byte_arr

def generate_random_email():
    """Генерирует случайный email"""
    username = ''.join(random.choices(string.ascii_lowercase, k=8))
    domain = random.choice(['test.com', 'example.com', 'demo.com'])
    return f"{username}@{domain}"

def generate_random_password():
    """Генерирует случайный пароль"""
    # Пароль должен содержать: заглавную букву, цифру, спецсимвол
    chars = string.ascii_lowercase + string.ascii_uppercase + string.digits
    password = ''.join(random.choices(chars, k=8))
    password += random.choice(string.ascii_uppercase)  # Заглавная
    password += random.choice(string.digits)  # Цифра
    password += random.choice('!@#$%^&*')  # Спецсимвол

    # Перемешиваем
    password_list = list(password)
    random.shuffle(password_list)
    return ''.join(password_list)

def register_user(email, password, sex_type):
    """Регистрирует пользователя"""
    url = f"{API_URL}/api/v1/authentication/register"
    data = {
        "email": email,
        "password": password,
        "sexType": sex_type
    }

    response = requests.post(url, json=data)
    if response.status_code == 200:
        print(f"✅ Registered: {email}")
        return True
    else:
        print(f"❌ Failed to register {email}: {response.status_code}")
        print(f"   Response: {response.text}")
        return False

def login_user(email, password):
    """Логинит пользователя и возвращает токен"""
    url = f"{API_URL}/api/v1/authentication/login"
    data = {
        "username": email,  # API uses 'username' field, not 'email'
        "password": password
    }

    response = requests.post(url, json=data)
    if response.status_code == 200:
        token = response.json().get('accessToken')
        print(f"✅ Logged in: {email}")
        return token
    else:
        print(f"❌ Failed to login {email}: {response.status_code}")
        return None

def create_talent(token):
    """Создаёт Talent для пользователя"""
    url = f"{API_URL}/api/v1/user/talent"
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }
    data = {
        "talentType": "Model",
        "talentDescription": "Professional model",
        "portfolioDescription": "My portfolio"
    }

    response = requests.post(url, json=data, headers=headers)
    if response.status_code == 200:
        print(f"✅ Created Talent")
        return True
    else:
        print(f"❌ Failed to create talent: {response.status_code}")
        print(f"   Response: {response.text}")
        return False

def update_profile(token, name):
    """Обновляет имя профиля"""
    url = f"{API_URL}/api/v1/user/profile/update"
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }
    data = {
        "name": name,
        "description": f"Модель из {random.choice(['Москвы', 'Санкт-Петербурга', 'Новосибирска', 'Екатеринбурга', 'Казани'])}"
    }

    response = requests.put(url, json=data, headers=headers)
    if response.status_code == 200:
        print(f"✅ Updated profile: {name}")
        return True
    else:
        print(f"❌ Failed to update profile: {response.status_code}")
        return False

def upload_photo(token, image_data, filename):
    """Загружает фотографию профиля"""
    url = f"{API_URL}/api/v1/user/media/upload"
    headers = {
        "Authorization": f"Bearer {token}"
    }

    files = {
        'file': (filename, image_data, 'image/jpeg')
    }

    response = requests.post(url, files=files, headers=headers)
    if response.status_code == 200:
        print(f"✅ Uploaded photo: {filename}")
        return True
    else:
        print(f"❌ Failed to upload photo: {response.status_code}")
        print(f"   Response: {response.text}")
        return False

def generate_model():
    """Генерирует одну модель с фотографией"""
    # Случайный пол
    sex_type = random.choice(["Male", "Female"])

    # Генерируем имя
    if sex_type == "Female":
        first_name = random.choice(FIRST_NAMES_FEMALE)
        last_name = random.choice(LAST_NAMES) + "а"  # Женская фамилия
    else:
        first_name = random.choice(FIRST_NAMES_MALE)
        last_name = random.choice(LAST_NAMES)

    full_name = f"{first_name} {last_name}"

    # Генерируем credentials
    email = generate_random_email()
    password = generate_random_password()

    print(f"\n{'='*60}")
    print(f"Generating model: {full_name} ({sex_type})")
    print(f"Email: {email}")
    print(f"Password: {password}")
    print(f"{'='*60}")

    # Регистрация
    if not register_user(email, password, sex_type):
        return False

    time.sleep(0.5)  # Небольшая задержка

    # Логин
    token = login_user(email, password)
    if not token:
        return False

    time.sleep(0.5)

    # Создание Talent
    if not create_talent(token):
        return False

    time.sleep(0.5)

    # Обновление профиля
    update_profile(token, full_name)  # Не критично если упадёт

    time.sleep(0.5)

    # Генерация и загрузка фото
    print(f"📸 Generating photo for {full_name}...")
    image_data = generate_random_image(full_name, sex_type)
    filename = f"{full_name.replace(' ', '_').lower()}.jpg"

    upload_photo(token, image_data, filename)  # Не критично если упадёт

    print(f"✨ Successfully generated model: {full_name}")
    return True

def main():
    """Основная функция"""
    print("🚀 Starting model generation...")
    print(f"Target: 30 models")
    print(f"API URL: {API_URL}")
    print()

    successful = 0
    failed = 0

    for i in range(30):
        print(f"\n📋 Progress: {i+1}/30")

        if generate_model():
            successful += 1
        else:
            failed += 1

        # Задержка между пользователями
        time.sleep(1)

    print("\n" + "="*60)
    print("📊 Generation complete!")
    print(f"✅ Successful: {successful}")
    print(f"❌ Failed: {failed}")
    print("="*60)

if __name__ == "__main__":
    main()
