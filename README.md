# Guideline

```bash
cd src/microservice-1

dapr run --app-id microservice-1 --app-port 6000 --port 3600 dotnet run

cd src/microservice-2

dapr run --app-id microservice-2 --app-port 7000 --port 3700 dotnet run
```
