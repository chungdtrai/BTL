const sizeButtons = document.querySelectorAll('.size-button');
const selectedSizeElement = document.getElementById('selected-size');

sizeButtons.forEach((button) => {
    button.addEventListener('click', () => {
        sizeButtons.forEach((btn) => {
            btn.classList.remove('selected');
        });
        button.classList.add('selected');
        const selectedSize = button.getAttribute('data-size');
        selectedSizeElement.textContent = selectedSize;
    });
});