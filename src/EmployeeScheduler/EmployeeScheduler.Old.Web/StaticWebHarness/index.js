(function () {
    const messageInput = document.getElementById('messageInput');
    const typeSelect = document.getElementById('typeSelect');
    const createButton = document.getElementById('createButton');

    createButton.addEventListener('click', e => {
        window.toast.create(messageInput.value, typeSelect.options[typeSelect.selectedIndex].value);
        messageInput.value = '';
    });

})();