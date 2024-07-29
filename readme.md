
# Tenders Filter

Tenders Filter is an REST API that puts on top of "url" additional features:

- Full pagination
- Filtering by tender Id
- Filtering by date
- Filtering by supplier Id
- Filtering by price in EUR
- Ordering by price in EUR in ascending or descending directions
- Ordering by date in ascending or descending directions

## Run locally

### Prerequisites

To start application locally, you need to have installed ASP.NET Core Runtime or SDK version at least 8.0.0 that can be downloaded from https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Starting application

- Open your favourite command line tool
- Navigate to the project FilterTenders.Apifolder
- Run command ```dotnet run```

### Running tests

- Navigate into the root of the project
- Run command Run ```dotnet test```

### Usage

Tender Filters API contain only one endpoint, that can be parametrised using query string parameters

Possible optional parameters:

- PageNumber - define which page number should be received
- PageSize - define how many records records on single page we want to received
- FilterByPriceInEuro - filters by exact price
- FilterByDate - filters by exact date
- FilterById - filters by exact tender id
- FilterBySupplierId - filters tender by exact supplier id that awarder the tender
- OrderBy - possible options are ```PriceInEuro``` OR ```Date``` (Case sensitive!) - orders result by specified column
- OrderType - possible options are ```Asc``` OR ```Desc``` (Case sensitive!) - apply ordering direction to the OrderBy. NB works only when ```OrderBy``` provided!!!

Example url
```
http://localhost:5046
/api/tenders?FilterByPriceInEuro=805.41&FilterById=586940&FilterByDate=2021-07-02T00:00:00&FilterBySupplierId=28415&OrderBy=PriceInEuro&OrderType=Desc&PageSize=100&PageNumber=1
```