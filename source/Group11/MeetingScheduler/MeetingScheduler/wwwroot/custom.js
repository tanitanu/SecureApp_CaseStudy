
function showInput(inputId) {
    const inputElement = document.getElementById(inputId);
    if (inputElement) {
        inputElement.style.display = 'block';
    }
}

function hideInput(inputId) {
    const inputElement = document.getElementById(inputId);
    if (inputElement) {
        inputElement.style.display = 'none';
    }
}
