#!/usr/bin/env python3
import os
import re

# Find all .cs files in FashionFace.Facades.Users/Implementations that use Guid.NewGuid()
base_path = "FashionFace.Facades.Users/Implementations"

for root, dirs, files in os.walk(base_path):
    for file in files:
        if file.endswith(".cs"):
            file_path = os.path.join(root, file)

            # Read file content
            with open(file_path, 'r', encoding='utf-8') as f:
                content = f.read()

            # Check if file uses Guid.NewGuid()
            if 'Guid.NewGuid()' in content:
                # Check if "using System;" already exists
                if not re.search(r'^using System;', content, re.MULTILINE):
                    print(f"Adding 'using System;' to {file_path}")

                    # Find the first using statement
                    lines = content.split('\n')
                    new_lines = []
                    inserted = False

                    for i, line in enumerate(lines):
                        # Insert "using System;" before the first non-BOM, non-comment, non-blank line
                        if not inserted and line.strip() and not line.strip().startswith('//'):
                            if line.strip().startswith('\ufeff'):
                                # BOM at start
                                new_lines.append(line)
                            elif line.strip().startswith('using'):
                                # Insert before first using
                                new_lines.append('using System;')
                                new_lines.append(line)
                                inserted = True
                            else:
                                # Insert before namespace or any other code
                                new_lines.append('using System;')
                                new_lines.append(line)
                                inserted = True
                        else:
                            new_lines.append(line)

                    # Write back
                    new_content = '\n'.join(new_lines)
                    with open(file_path, 'w', encoding='utf-8') as f:
                        f.write(new_content)
                else:
                    print(f"Skipping {file_path} - already has 'using System;'")

print("Done adding 'using System;' statements")
