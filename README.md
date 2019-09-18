# Start-Me

[![CircleCI](https://circleci.com/gh/remvee/start-me.svg?style=svg)](https://circleci.com/gh/remvee/start-me)

A safe-ish alternative to `file:` URLs by providing a basic file association
handler to allow opening local files from a restricted directory from a web
page.

## What?

Say you have a file share and both the client and webserver computer have
access to it.  This little app allows opening a file on that share from a page
from the webserver by responding with a `.start-me` file which holds the
location of the file.  These `.start-me` files will be associated with this
application, when launched they'll start this app which will look it up in the
share and run `Process.Start` given that location to open it.

## Installation

Compile `start_me_handler.cs` to `start_me_handler.exe` with you favorite C#
compiler and run `install.bat` as administrator.  Edit the
`start_me_handler.ini` to point it to the file share and configure the timeout
for waiting for the file to appear.

## Server

Respond with a something like:

    Content-Type: application/x-start-me
    Content-Disposition: attachment; filename="please.start-me"
    
    some-directory/sub/other/document.docx
    
## License

Copyright (c) 2019-.. R.W. van 't Veer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
