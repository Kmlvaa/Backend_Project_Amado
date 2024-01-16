var titleLinks = document.querySelectorAll('.title-link');
var brandLinks = document.querySelectorAll('.brand-link');
var colorLinks = document.querySelectorAll('.color-link');
var navLinks = document.querySelectorAll('.nav-link');

navLinks.forEach(item => {
    item.addEventListener('click', event => {
        navLinks.forEach(i => { i.classList.remove('active') })
        item.classList.add('active')
    })
})

    titleLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            event.preventDefault();

            titleLinks.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            this.classList.add('active');

            var titleid = this.getAttribute('data-titleId');

            fetch('/shop/sorted?titleId=' + titleid)
                .then(response => response.text())
                .then(data => {
                    var partialContainer = document.getElementById('partials');
                    partialContainer.innerHTML = data;
                })
                .catch(error => {
                    console.error(error);
                });
        });
    });

    brandLinks.forEach(function (link) {
            link.addEventListener('click', function (event) {
                event.preventDefault();

                brandLinks.forEach(function (otherLink) {
                    otherLink.classList.remove('active');
                });

                this.classList.add('active');

                var brandid = this.getAttribute('data-brandId');

                fetch('/shop/sorted?brandId=' + brandid)
                    .then(response => response.text())
                    .then(data => {
                        var partialContainer = document.getElementById('partials');
                        partialContainer.innerHTML = data;
                    })
                    .catch(error => {
                        console.error(error);
                    });
            });
        });

        colorLinks.forEach(function (link) {
            link.addEventListener('click', function (event) {
                event.preventDefault();


                var colorid = this.getAttribute('data-colorId');

                fetch('/shop/sorted?colorId=' + colorid)
                    .then(response => response.text())
                    .then(data => {
                        var partialContainer = document.getElementById('partials');
                        partialContainer.innerHTML = data;
                    })
                    .catch(error => {
                        console.error(error);
                    });
            });
        });

