(function () {
    document.querySelectorAll('.hz-mini-map').forEach(function (element) {
        if (typeof L === 'undefined') {
            return;
        }

        var map = L.map(element, { scrollWheelZoom: false });

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 17,
            attribution: '&copy; OpenStreetMap contributors'
        }).addTo(map);

        if (element.dataset.points) {
            var points = JSON.parse(element.dataset.points);
            var line = L.polyline(points, { color: '#e07a32', weight: 5 }).addTo(map);

            L.circleMarker(points[0], { radius: 7, color: '#1f4d3a', fillOpacity: 1 }).addTo(map);
            L.circleMarker(points[points.length - 1], { radius: 7, color: '#c8432f', fillOpacity: 1 }).addTo(map);

            map.fitBounds(line.getBounds(), { padding: [25, 25] });
        } else {
            var lat = parseFloat(element.dataset.lat);
            var lng = parseFloat(element.dataset.lng);

            map.setView([lat, lng], 13);
            L.marker([lat, lng]).addTo(map);
        }
    });
})();