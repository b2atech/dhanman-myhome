console.log('custom.qa.js loaded'); // sanity check
document.title = "Community - QA"; // ✅ Set custom page title

(function insertBadgeWhenReady() {
    const h2 = document.querySelector('.swagger-ui .title');

    if (h2 && !document.querySelector('.env-badge')) {
        const badge = document.createElement('span');
        badge.innerText = 'QA';
        badge.className = 'env-badge';
        badge.style.backgroundColor = '#ffc107';
        badge.style.color = '#000';
        badge.style.fontWeight = 'bold';
        badge.style.padding = '2px 8px';
        badge.style.marginLeft = '10px';
        badge.style.borderRadius = '12px';
        badge.style.fontSize = '12px';
        badge.style.verticalAlign = 'middle';
        badge.style.display = 'inline-block';
        badge.style.boxShadow = '0 0 4px rgba(0,0,0,0.1)';
        badge.style.fontFamily = 'sans-serif';

        // Place badge right after the title text
        const textNode = h2.childNodes[0];
        if (textNode && textNode.nodeType === Node.TEXT_NODE) {
            h2.insertBefore(badge, textNode.nextSibling);
        } else {
            h2.appendChild(badge);
        }

        console.log('✅ Badge injected');
    } else {
        // Try again after 200ms until h2 appears
        setTimeout(insertBadgeWhenReady, 200);
    }
})();
