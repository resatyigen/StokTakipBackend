docker run -p 5000:80 -v C:/Users/Resat/Documents/DockerVolume:/app/images web_api_test_4
docker run -p 5000:80 --link MySQL:db -v C:/Users/Resat/Documents/DockerVolume:/app/images web_api_test_8

dotnet publish -c Release  
docker build -t web_api_test_5 .

docker run --name mysql-server -e MYSQL_USER=local -e MYSQL_PASSWORD=123456 -e MYSQL_ROOT_PASSWORD=root -v /home/MySql:/var/lib/mysql -p 3306:3306 -p 33060:33060 mysql

docker run --name web_api_v1.0.4 -p 5000:80 -v /home/rakunsoft/StokTakip/images:/app/images --link mysql-server:localhost web_api_v1.0.4

docker run --name phpmyadmin -d --link mysql-server:db -p 8098:80 phpmyadmin

May 20 20:27:51 5.180.107.95 multipathd[653]: sda: failed to get udev uid: Invalid argument

Sunucu saati yanlış ayarlanmış.