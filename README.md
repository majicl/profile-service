# Profile Service API

Profile Service API is resposible to manage customer profile.

## Live Demo

[https://my-profile-service.azurewebsites.net/](https://my-profile-service.azurewebsites.net/).

## Usage

```http
POST /api/profile
```

example value:

```json
{
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "ssn": "string",
  "countryCode": "string",
  "phoneNumber": "string",
  "email": "string"
}
```

```http
PUT /api/profile
```

example value:

```json
{
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "ssn": "string",
  "countryCode": "string",
  "phoneNumber": "string",
  "email": "string",
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

```http
DELETE /api/profile
```

example value:

```json
{
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

And

```http
GET /api/profile/{customerId}
```

## Available Scripts

It is used to install all dependencies for a project.<br />
**It is necessary to run before staring the app**.

#### `dotnet restore`

In the API project directory, you can run:

#### `dotnet run`

Runs the app in the development mode.<br />
Open [https://localhost:4001](https://localhost:4001) to view it in the browser.

### How to run the test?

#### `dotnet test`

### How to publish?

#### `dotnet publish -c Release -r win-x64 -o ./output --self-contained`

## Containerize the app

### How to build a Docker image that contains the .NET Core application with running the tests.

#### `docker image build -t sambose/profile-service .`

### and run it by:

#### `docker run -d -p 8080:80 --name my-api sambose/profile-service`

## Architecture and Libraries

### dotnet core 3.0, Clean architecture, CQRS, entity framework core 3.1, FluentValidation, Swagger, xUnit, docker
