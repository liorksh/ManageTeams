FROM microsoft/aspnetcore:2.0

WORKDIR /app
RUN mkdir -p /app/run

COPY ./publish .

ENTRYPOINT ["dotnet", "TeamMngtWS.dll"]
