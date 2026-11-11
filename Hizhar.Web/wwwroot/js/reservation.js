(function () {
    var form = document.getElementById('reservation-form');
    if (!form) {
        return;
    }

    var hutId = form.dataset.hutId;
    var pricePerNight = parseFloat(form.dataset.price);
    var checkIn = form.querySelector('[name="CheckIn"]');
    var checkOut = form.querySelector('[name="CheckOut"]');
    var guests = form.querySelector('[name="Guests"]');
    var freeBedsInfo = document.getElementById('free-beds-info');
    var totalInfo = document.getElementById('total-price-info');

    function nightsBetween(from, to) {
        var milliseconds = new Date(to) - new Date(from);
        return Math.round(milliseconds / (1000 * 60 * 60 * 24));
    }

    function updateTotal() {
        var nights = nightsBetween(checkIn.value, checkOut.value);
        var count = parseInt(guests.value, 10) || 0;

        if (nights > 0 && count > 0) {
            var total = nights * count * pricePerNight;
            totalInfo.textContent = nights + ' нощ(и) × ' + count + ' легл(а) = ' + total.toFixed(2) + ' лв.';
        } else {
            totalInfo.textContent = '';
        }
    }

    function updateFreeBeds() {
        updateTotal();

        if (!checkIn.value || !checkOut.value || checkOut.value <= checkIn.value) {
            freeBedsInfo.textContent = '';
            return;
        }

        var url = '/Reservations/FreeBeds?hutId=' + hutId +
            '&checkIn=' + checkIn.value + '&checkOut=' + checkOut.value;

        fetch(url)
            .then(function (response) { return response.json(); })
            .then(function (data) {
                freeBedsInfo.textContent = 'Свободни легла за периода: ' + data.freeBeds;
                freeBedsInfo.className = data.freeBeds > 0 ? 'text-success fw-semibold' : 'text-danger fw-semibold';
            })
            .catch(function () {
                freeBedsInfo.textContent = '';
            });
    }

    checkIn.addEventListener('change', updateFreeBeds);
    checkOut.addEventListener('change', updateFreeBeds);
    guests.addEventListener('input', updateTotal);

    updateFreeBeds();
})();