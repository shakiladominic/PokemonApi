### Pokemon Api ###
This repository contains endpoints that can get basic pokemon details such as name,description,habitat and legendary status. Pokemon's description can be translated into a fun description using Shakespeare or Yoda translation services using the /translated endpoint. 

### Run and Debug in your local machine ###
To run this repository in your local machine, you need the below dependencies:
* .Net Core 5.0 (can be downlaoded from https://dotnet.microsoft.com/download/dotnet/5.0)
*  Any .Net5.0 compatible IDE(Recommended - Visual Studio 2019)  

### Swagger ###
To test the endpoints in your local machine, run the project and a swagger page should open for you. 
https://localhost:5001/swagger/index.html

Note: You can use 'mewtwo' or 'wormadam' as Pokemon names.

### Dependencies ###
This project relies on below public Apis to get the pokemon details and to translate the description. It is important these endpoints are up and working for the Pokemon project to return a valid response.

* Basic Pokemon Api : https://pokeapi.co/api/v2/pokemon-species(HttpGet)
* Shakespeare Translation Api : https://api.funtranslations.com/translate/shakespeare.json(HttpPost)
* Yoda Translation Api : https://api.funtranslations.com/translate/yoda.json(HttpPost)

### Logging Setup  ###
As this is a test project, logging has not been implemented. For a productionised version of this code, logging integration along with logging level should be configured(Info/Debug/Error)

### Configuration Service  ###
As this is a test project, configuration has not been implemented. Public Api Urls used in the project are a good candidate for moving into Configuration. 