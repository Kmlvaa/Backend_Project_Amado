
    var titleLinks = document.querySelectorAll('.title-link');
    var brandLinks = document.querySelectorAll('.brand-link');
    var colorLinks = document.querySelectorAll('.color-link');

    titleLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            event.preventDefault();

            titleLinks.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            this.classList.add('active');

            var titleid = this.getAttribute('data-titleid');

            fetch('/shop/sorted?titleid=' + titleid)
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

                var brandid = this.getAttribute('data-brandid');

                fetch('/shop/sorted?brandid=' + brandid)
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


                var colorid = this.getAttribute('data-colorid');

                fetch('/shop/sorted?colorid=' + colorid)
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

