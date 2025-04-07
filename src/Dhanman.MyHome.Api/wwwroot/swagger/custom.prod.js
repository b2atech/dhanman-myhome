console.log('custom.prod.js loaded'); // sanity check

(function insertBadgeWhenReady() {
    const h2 = document.querySelector('.swagger-ui .title');

    if (h2 && !document.querySelector('.env-badge')) {
        const badge = document.createElement('span');
        badge.innerText = 'PROD';
        badge.className = 'env-badge';
        badge.title = 'Production Environment - Live and Active!';
        badge.style.backgroundColor = '#dc3545'; // Bootstrap red
        badge.style.color = '#fff';
        badge.style.fontWeight = 'bold';
        badge.style.padding = '2px 8px';
        badge.style.marginLeft = '10px';
        badge.style.borderRadius = '12px';
        badge.style.fontSize = '12px';
        badge.style.verticalAlign = 'middle';
        badge.style.display = 'inline-block';
        badge.style.boxShadow = '0 0 4px rgba(0,0,0,0.1)';
        badge.style.fontFamily = 'sans-serif';

        const textNode = h2.childNodes[0];
        if (textNode && textNode.nodeType === Node.TEXT_NODE) {
            h2.insertBefore(badge, textNode.nextSibling);
        } else {
            h2.appendChild(badge);
        }

        console.log('✅ PROD badge injected');
    } else {
        setTimeout(insertBadgeWhenReady, 200);
    }
})();
