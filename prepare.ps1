$shellPath = Get-Location

$pathArray = @();
$pathArray += Join-Path $shellPath "\Assets\Scripts"
$pathArray += Join-Path $shellPath "\Assets\Textures"
$pathArray += Join-Path $shellPath "\Assets\Prefabs"
$pathArray += Join-Path $shellPath "\Assets\Fonts"
$pathArray += Join-Path $shellPath "\Assets\Resources"
$pathArray += Join-Path $shellPath "\Etc"
$pathArray += Join-Path $shellPath "\Etc\PHP"
$pathArray += Join-Path $shellPath "\Etc\FixData"
$pathArray += Join-Path $shellPath "\Etc\Žd—l"
foreach($path in $pathArray){
    if( Test-Path $path ){

    }else{
        New-Item $path -ItemType Directory
    }
}
