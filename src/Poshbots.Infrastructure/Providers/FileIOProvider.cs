using Poshbots.Core.Providers;
using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Poshbots.Infrastructure.Providers
{
    public class FileIOProvider : IFileIOProvider
    {
        public string ReadFile(string path)
        {
            return @"$BaseUri = ""https://poshbots.com/""
$GameName = $null
$Player1Name = $null
$Player1Memory = $null;
$Player2Name = $null
$Player2Memory = $null;

function Get-Surroundings 
{
    Param([string] $playerName)
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/GetSurroundings/$GameName/?playerName=$playerName""
}

function Get-Score 
{
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/GetScore/$GameName""
}

function Move-Up
{
    Param([string] $playerName)
    ""$playerName moves up""
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/MoveUp/$GameName/?playerName=$playerName""
}

function Move-Down
{
    Param([string] $playerName)
    ""$playerName moves down""
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/MoveDown/$GameName/?playerName=$playerName""
}

function Move-Left
{
    Param([string] $playerName)
    ""$playerName moves left""
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/MoveLeft/$GameName/?playerName=$playerName""
}

function Move-Right
{
    Param([string] $playerName)
    ""$playerName moves right""
    Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/MoveRight/$GameName/?playerName=$playerName""
}

function Move-Player1 { }

function Move-Player2 { }

$currentMap = Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/map/$GameName""
while($currentMap.Winner -eq $null)
{
    Move-Player1
    Move-Player2

    $currentMap = Invoke-RestMethod -Method Get -Uri ""$BaseUri/api/map/$GameName""
	
	Start-Sleep -Milliseconds 500
}";
            /*using (StreamReader stream = new StreamReader(VirtualPathProvider.OpenFile("~/" + path)))
            {
                return stream.ReadToEnd();
            }*/
        }
    }
}
