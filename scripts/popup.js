function popupMax(page) {

    if (window.innerWidth == undefined) {
        iWidth = document.body.clientWidth;
        iHeight = document.body.clientHeight;
    }
    else {
        iWidth = window.innerWidth;
        iHeight = window.innerHeight;
    }

    width = iWidth - 110; height = iHeight - 80;
    xposition = window.screenLeft + 55; yposition = window.screenTop + 60;


    args = "width=" + width + "," + "height=" + height + ","
    + "location=0," + "menubar=0," + "resizable=1,"
    + "scrollbars=1," + "status=0," + "titlebar=0," + "toolbar=0,"
    + "hotkeys=0," + "screenx=" + xposition + ","  //NN Only 
    + "screeny=" + yposition + ","  //NN Only 
    + "left=" + xposition + ","     //IE Only 
    + "top=" + yposition;           //IE Only 

    mapWindow = window.open(page, 'mapWinM', args);
    //alert(mapWindow);
    //mapWindow.focus();
    return false;
    //return mapWindow ? false : true; // allow the link to work if popup is blocked
}

/*function popupMax(page) {

    width = screen.width - 110; height = screen.height - 160;
    xposition = 50; yposition = 50;


    args = "width=" + width + "," + "height=" + height + ","
    + "location=0," + "menubar=0," + "resizable=1,"
    + "scrollbars=1," + "status=0," + "titlebar=0," + "toolbar=0,"
    + "hotkeys=0," + "screenx=" + xposition + ","  //NN Only 
    + "screeny=" + yposition + ","  //NN Only 
    + "left=" + xposition + ","     //IE Only 
    + "top=" + yposition;           //IE Only 

    mapWindow = window.open(page, 'mapWinM', args);
    //alert(mapWindow);
    //mapWindow.focus();
    return false;
    //return mapWindow ? false : true; // allow the link to work if popup is blocked
}*/