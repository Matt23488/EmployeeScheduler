(function () {

    class ToastManager {
        create(message, type) {
            const typeVal = parseInt(type);

            const toast = document.createElement('li');
            toast.classList.add('em-toast');
            toast.classList.add('list-group-item');
            toast.classList.add(getToastTypeCssClass(typeVal));
            toast.innerText = message;

            const toastArea = document.getElementById('toastArea');
            toastArea.appendChild(toast);

            window.setTimeout(() => toast.remove(), 8010);
            toast.addEventListener('click', e => toast.remove());
        }
    }

    function getToastTypeCssClass(type) {
        switch (type) {
            case 0: return 'list-group-item-info';
            case 1: return 'list-group-item-success';
            case 2: return 'list-group-item-warning';
            case 3: return 'list-group-item-danger';
        }
    }

    window.toast = new ToastManager();

})();