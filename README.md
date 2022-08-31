# Rock Scissor Paper - The game
## Requirements
[.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
## Build & Run
The application targets .NET 6 and can be built using the following commands, from the root directory:
```
dotnet publish -o RockScissorPaper -c Release RockScissorsPaper.Api/RockScissorsPaper.Api.csproj
dotnet RockScissorsPaper/RockScissorsPaper.Api.dll
```
## Usage
The application provides a Swagger UI, which shows what input the api accepts. It can be found by going to: [http://localhost:5000/swagger](http://localhost:5000/swagger) in a modern browser.
The game accepts two players, and does not allow rematches.
## Example
To test the game using [curl](https://curl.se/download.html), the following commands can be run.\
Here using a bash script:
```bash
gameId=$(curl -s -H 'Content-Type: application/json' -d '{"player": "John"}' http://localhost:5000/Games)

curl -s -H 'Content-Type: application/json' -d '{"player": "Jane"}' http://localhost:5000/Games/{$gameId}/Join

curl -s -H 'Content-Type: application/json' -d '{"player": "Jane", "move" : "Rock"}' http://localhost:5000/Games/{$gameId}/Move

curl -s -H 'Content-Type: application/json' -d '{"player": "John", "move" : "Scissors"}' http://localhost:5000/Games/{$gameId}/Move

curl -s http://localhost:5000/Games/{$gameId}
```
## Tests
The tests are built and run using the following command, standing in the root directory:
```
dotnet test
```
