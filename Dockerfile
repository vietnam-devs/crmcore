FROM microsoft/dotnet:2.0

ENV ASPNETCORE_URLS http://+:5000

WORKDIR /app

COPY build/release .

CMD dotnet --version