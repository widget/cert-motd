#!/bin/bash
# 
# Install script, nothing fancy

DB_DIR="/usr/share/fortune/cert_motd"
DB_NAME="db.json"

# Ensure the directory exists
if [ ! -d $DB_DIR ]; then
	mkdir -p $DB_DIR
fi

# Run the generator
python3 cert-db-generator.py -o $DB_DIR/$DB_NAME
# Install the fortune program
install fortune-cert.py /usr/bin
