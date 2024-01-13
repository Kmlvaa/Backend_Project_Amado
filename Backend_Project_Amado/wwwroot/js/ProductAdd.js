const imgInput = document.querySelector('.img-input');
const dynamicImages = document.querySelector('.dynamicImages');

imgInput.addEventListener('change', (e) => {
    let files = e.target.files;
    dynamicImages.innerHTML = '';

    for (const file in files) {
        const img = document.createElement("img");

        const blobUrl = URL.createObjectURL(file);

        img.setAttribute('src', blobUrl);

        dynamicImages.appendChild(img);
    }
})