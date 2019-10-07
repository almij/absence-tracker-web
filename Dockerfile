FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
EXPOSE 80

WORKDIR /src

COPY ./AbsenceTrackerWeb/AbsenceTrackerWeb.sln ./
COPY ./AbsenceTrackerWeb/AbsenceTrackerLibrary/*.csproj ./AbsenceTrackerLibrary/
COPY ./AbsenceTrackerWeb/AbsenceTrackerMVC/*.csproj ./AbsenceTrackerMVC/
RUN dotnet restore

COPY ./AbsenceTrackerWeb/ ./
WORKDIR /src/AbsenceTrackerLibrary/
RUN dotnet build -c Release -o /app

WORKDIR /src/AbsenceTrackerMVC/
RUN dotnet build -c Release -o /app

FROM build-env AS publish
RUN dotnet publish -c Release -o /app


FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AbsenceTrackerMVC.dll