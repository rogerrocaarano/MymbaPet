#!/bin/bash

if [ $# -lt 1 ]; then
    echo "Please provide the new service name as the first argument."
    exit 1
fi

# Configure the service
new_service_name="$1.service"

mv $2/$1/conf/linux.service.conf "/etc/systemd/system/$new_service_name"
chown root:root "/etc/systemd/system/$new_service_name"
chmod 644 "/etc/systemd/system/$new_service_name"

systemctl daemon-reload
systemctl enable "$new_service_name"

# Configure nginx
mv $2/$1/conf/nginx.conf "/etc/nginx/sites-available/$1"
chown root:root "/etc/nginx/sites-available/$1"
chmod 644 "/etc/nginx/sites-available/$1"
ln -s "/etc/nginx/sites-available/$1" "/etc/nginx/sites-enabled/$1"

# start application
systemctl start "$new_service_name"
systemctl restart nginx