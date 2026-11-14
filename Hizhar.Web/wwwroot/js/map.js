(function () {
    var mapElement = document.getElementById('hz-map');
    if (!mapElement || typeof L === 'undefined') {
        return;
    }

    var map = L.map('hz-map').setView([42.4, 24.2], 8);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 17,
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    var trailColors = { Easy: '#2e9d5b', Medium: '#e0a526', Hard: '#c8432f' };

    var hutIcon = L.divIcon({
        className: '',
        html: '<div style="background:#1f4d3a;color:#fff;border-radius:50%;width:30px;height:30px;display:flex;align-items:center;justify-content:center;border:2px solid #fff;box-shadow:0 2px 6px rgba(0,0,0,.3)"><i class="bi bi-house-fill"></i></div>',
        iconSize: [30, 30],
        iconAnchor: [15, 15]
    });

    var peakIcon = L.divIcon({
        className: '',
        html: '<div style="background:#e07a32;color:#fff;border-radius:50%;width:28px;height:28px;display:flex;align-items:center;justify-content:center;border:2px solid #fff;box-shadow:0 2px 6px rgba(0,0,0,.3)"><i class="bi bi-triangle-fill" style="font-size:.75rem"></i></div>',
        iconSize: [28, 28],
        iconAnchor: [14, 14]
    });

    function escapeHtml(text) {
        var div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    var hutsLayer = L.layerGroup().addTo(map);
    var peaksLayer = L.layerGroup().addTo(map);
    var trailsLayer = L.layerGroup().addTo(map);

    fetch('/Map/Data')
        .then(function (response) { return response.json(); })
        .then(function (data) {
            data.trails.forEach(function (trail) {
                if (trail.coordinates.length < 2) {
                    return;
                }

                L.polyline(trail.coordinates, {
                    color: trailColors[trail.difficulty] || '#555',
                    weight: 5,
                    opacity: 0.85
                })
                    .bindPopup('<strong>' + escapeHtml(trail.name) + '</strong><br><a href="/Trails/Details/' + trail.id + '">Виж маршрута</a>')
                    .addTo(trailsLayer);
            });

            data.huts.forEach(function (hut) {
                L.marker([hut.latitude, hut.longitude], { icon: hutIcon })
                    .bindPopup('<strong>' + escapeHtml(hut.name) + '</strong><br>' + hut.elevation + ' м<br><a href="/Huts/Details/' + hut.id + '">Виж хижата</a>')
                    .addTo(hutsLayer);
            });

            data.peaks.forEach(function (peak) {
                L.marker([peak.latitude, peak.longitude], { icon: peakIcon })
                    .bindPopup('<strong>' + escapeHtml(peak.name) + '</strong><br>' + peak.elevation + ' м<br><a href="/Peaks/Details/' + peak.id + '">Виж върха</a>')
                    .addTo(peaksLayer);
            });
        })
        .catch(function () {
            mapElement.insertAdjacentHTML('beforebegin', '<div class="alert alert-warning">Данните за картата не можаха да се заредят.</div>');
        });

    L.control.layers(null, {
        'Хижи': hutsLayer,
        'Върхове': peaksLayer,
        'Маршрути': trailsLayer
    }).addTo(map);
})();