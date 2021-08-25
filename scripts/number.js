function validateNumericOnly(field, max) {
    var valid = "0123456789"
    var temp;
    for (var i = 0; i < field.value.length; i++) {
        temp = "" + field.value.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1") {
            if (i == (field.value.length - 1))
                field.value = field.value.substring(0, i);
            else
                field.value = field.value.substring(0, i)
                        + field.value.substring(i + 1, field.value.length);
            i = i - 1;
        }
    }
    if (max > 0) {
        while (field.value > max) {
            field.value = field.value.substring(0, field.value.length - 1)
        }
    }
}

function letterToEmptyPopupOutRange(field, min, max, fieldName) {
    if (!isNumber(field.value))
        field.value = "";
    else if (field.value > max || field.value < min) {
        alert(fieldName + " must to be between " + min + " and " + max);
        window.setTimeout(function () {
            field.focus();
        }, 0);
        return false;
    }
}

function deletePaste(field) {
    field.value = "";
    field.focus();
}

// Function Name: isDigit
function isDigit(chr) {
    if (chr.charCodeAt(0) < 48 || chr.charCodeAt(0) > 57)
        return false;
    return true;
}

// Function Name: isNumber
function isNumber(str) {

    if (str.length == 0) return false
    for (i = 0; i < str.length; i++) {
        if (isDigit(str.charAt(i)) == false)
            return false;

    }
    return true;
}