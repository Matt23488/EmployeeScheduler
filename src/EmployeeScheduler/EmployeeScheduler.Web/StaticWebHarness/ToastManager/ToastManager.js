(function () {

    const toastArea = document.getElementById('toastArea');

    class ToastManager {
        create(message, type) {
            const typeVal = parseInt(type);

            const toast = document.createElement('div');
            toast.classList.add('toast');
            toast.classList.add(getToastTypeCssClass(typeVal));
            toast.innerText = message;

            toastArea.appendChild(toast);

            window.setTimeout(() => toast.remove(), 5000);
            toast.addEventListener('click', e => toast.remove());
        }
    }

    function getToastTypeCssClass(type) {
        switch (type) {
            case 0: return 'toast-info';
            case 1: return 'toast-success';
            case 2: return 'toast-warning';
            case 3: return 'toast-error';
        }
    }

    window.toast = new ToastManager();

})();