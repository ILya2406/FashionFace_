#!/bin/bash

# Wait for RabbitMQ to start
sleep 10

# Create vhost if it doesn't exist
rabbitmqctl add_vhost vhost || true

# Set permissions
rabbitmqctl set_permissions -p vhost rabbitmq ".*" ".*" ".*"

echo "RabbitMQ initialization completed"
