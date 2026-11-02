document.querySelectorAll(".chapter-title").forEach(function (item) {

    item.addEventListener("click", function () {

        let current = this.parentElement;

        document.querySelectorAll(".chapter").forEach(function (ch) {

            if (ch != current)
                ch.classList.remove("open");

        });

        current.classList.toggle("open");

    });

});