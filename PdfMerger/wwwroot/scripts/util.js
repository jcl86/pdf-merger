window.blazorExtensions = {
    saveAsFile: function (filename, bytesBase64) {
        var link = document.createElement('a');
        link.download = filename;
        link.href = "data:application/octet-stream;base64," + bytesBase64;
        document.body.appendChild(link); // Needed for Firefox
        link.click();
        document.body.removeChild(link);
    },
    setValueFromElement: function (id, newValue) {
        let e = document.getElementById(id);
        e.value = newValue;
    }
};
