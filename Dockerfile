FROM microsoft/dotnet:2.0-runtime

ENV ASPNETCORE_URLS http://+:5000

WORKDIR /app

COPY build/release .

ENTRYPOINT [ "dotnet", "CRMCore.WebApp.dll" ]