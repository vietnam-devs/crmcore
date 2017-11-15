FROM microsoft/dotnet:2.0-sdk

ENV ASPNETCORE_URLS http://+:5000
ENV ASPNETCORE_ENVIRONMENT Development

WORKDIR /app

COPY build/release .

#ENTRYPOINT ["dotnet", "CRMCore.WebApp.dll"]
ENTRYPOINT [ "bash" ]
