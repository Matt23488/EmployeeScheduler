(function () {

    const timePickerObj = {};

    timePickerObj.backspace = function (element) {
        element.value = element.value.substring(0, element.value.length - 1);
        return element.value;
    };

    timePickerObj.addCharacter = function (element, character) {
        element.value += character;
        return element.value;
    };

    timePickerObj.toUpper = function (element) {
        element.value = element.value.toUpperCase();
        return element.value;
    };

    window.timePickerHelper = timePickerObj;

})();