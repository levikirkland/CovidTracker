# CovidTracker
<img src="https://img.shields.io/circleci/project/github/badges/shields/master" alt="build status"></a>
    <a href="https://circleci.com/gh/badges/daily-tests">

Sample Blazor App
Visit [Covid Tracker](https://statecovidtracker.azurewebsites.net/)
### Logging
#### Datalust/SEQ
run command in powershell if SEQ not installed
#### Docker Command
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest


Or remove SEQ from program.cs, allow only console logging.

### Unit Tests
Only testing a compare for the model Equals/Hash. Necessary for data comparison should more tests be made.

### Client
CovidTracker.Client using a Client Factory, with Polly resiliancy. Custom Json Deserializer to map fields from json stream to StateResponse. AutoMapper used to map to front end CovidStateModel.

### UI
UI uses MudBlazor as the UI framework. Dates allowed for selection fall into the range of the Covid data.
Uncheck the Use Date In Search option to search for current Covid data by State Only. Select to search by both data and state.

### Release
Pull requests merge into main and will release into Azure. [Covid Tracker](https://statecovidtracker.azurewebsites.net/)


