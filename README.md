Open Command Prompt and run:

git clone https://github.com/ViswanthSwarna/Products.git


Open appsettings.json at Products/Products.API/appsettings.Json

And Change connection string to your Database Server Connection String and path under serilog to your prefered path

![image](https://github.com/user-attachments/assets/ed4e4a90-888d-41a8-83c9-405c6e0cd849)


then in the same Command Prompt we cloned the project

Run

cd Projects/Products.API

dotnet ef database update

dotnet run --urls "http://localhost:5050"

Finally go to url http://localhost:5050/Swagger and test the endpoints 
![image](https://github.com/user-attachments/assets/a12f3511-7ce9-423a-a2a7-c1dd3b6b9887)





