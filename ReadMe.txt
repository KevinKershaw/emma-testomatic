Setting up IE
for ie you need to set Settings -> Advance -> Security Section -> Check-Allow active content to run files on My Computer*
registry setting?
for ie 11

HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE
"iexplore.exe"=dword:00000000

http://stackoverflow.com/questions/23782891/selenium-webdriver-on-ie11

https://code.google.com/p/selenium/wiki/InternetExplorerDriver#Required_Configuration

