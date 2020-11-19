Before running the project locally ensure that the following setup is created:

SQL DB:
 - Configure the connection string "SqlConnectionString" to point to an existing database
 - Create the necessary table by running the following command on the DB:
```
CREATE TABLE [dbo].[WeatherForecast] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [City]        NVARCHAR (150) NOT NULL,
    [Temperature] FLOAT (53)     NOT NULL,
    [Humidity]    FLOAT (53)     NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Time]        DATETIME       NOT NULL
);
```

Storage Account:
 - Configure the connection string "AzureWebJobsStorage" to point to an existing storage account (or use the development env) 
 - Navigate to the storage account (recommended using Azure Storage Explorer)
 - Create a folder in the Blob Containers named "forecast-reports"
