FROM microsoft/aspnetcore:2.0

WORKDIR /app
RUN mkdir -p /app/run
RUN dotnet build
RUN dotnet publish -o ./publish

COPY ./publish .

ENTRYPOINT ["dotnet", "TeamMngtWS.dll"]
