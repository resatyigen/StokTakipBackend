docker run -p 5000:80 -v C:/Users/Resat/Documents/DockerVolume:/app/images web_api_test_4
docker run -p 5000:80 --link MySQL:db -v C:/Users/Resat/Documents/DockerVolume:/app/images web_api_test_8

dotnet publish -c Release  
docker build -t web_api_test_5 . 



May 20 20:27:51 5.180.107.95 multipathd[653]: sda: failed to get udev uid: Invalid argument

Sunucu saati yanlış ayarlanmış.