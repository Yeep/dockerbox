New-Item -Path . -Name "Images" -ItemType "directory" -Force | Out-Null

for ($i=1; $i -le 15; $i++)
{
    $json = Invoke-WebRequest "https://commons.wikimedia.org/w/api.php?action=query&generator=random&grnnamespace=6&prop=imageinfo&iiprop=url&iiurlwidth=640&format=json"
    $response = ConvertFrom-Json $json

    $response.query.pages.PSObject.Properties | ForEach-Object { Invoke-WebRequest -Uri $_.Value.imageinfo.thumburl -OutFile Images/$([System.Web.HttpUtility]::UrlDecode($(Split-Path -Path $_.Value.imageinfo.thumburl -Leaf)))  }
}