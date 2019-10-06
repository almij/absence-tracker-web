FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./AbsenceTrackerWeb/AbsenceTrackerMVC/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./AbsenceTrackerWeb/AbsenceTrackerMVC/ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AbsenceTrackerMVC.dll