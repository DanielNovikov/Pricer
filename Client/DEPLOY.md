## How to deploy on ubuntu
##### Download docker 
- link with instructions https://docs.docker.com/engine/install/ubuntu/
- verify if it was installed by command `docker-compose -v`
- enable docker by command `systemctl enable docker`

##### Install and configure nginx
- run command `apt install nginx`
- open nginx configuration `vi /etc/nginx/sites-available` and paste:
    ```sh
    server_name pricker.ink www.pricer.ink
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarder_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
    ```
- install certbot by commands 
    ```sh
    apt-get install certbot
    add-apt-repository ppa:certbot/certbot
    apt-get install python3-certbot-nginx 
    ```
- install ufw by command `apt install ufw` (if needed `apt install software-properties-common`)
- configure ufw with following: 
    ```sh
    ufw default deny incoming
    ufw default allow outgoing
    ufw allow ssh
    ufw allow 22
    ufw allow 443
    ufw allow 80
    ufw allow 'Nginx Full'
    ufw enable
    ```
- create certificate with command `certbot --nginx -d pricer.ink -d www.pricer.ink`

#### Run application
- go to ***/var/project***, copy **docker-compose.yml** and set environment variables
- run `docker-compose pull`
- run `docker-compose up -d`
