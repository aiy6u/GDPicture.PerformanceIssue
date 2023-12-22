// Start reload issue

function reloadIssue() {
    document.getElementById('process-status').innerText = 'Reproducing issue...';
    let attemptReload = 0;
    const processId = setInterval(() => {
        document.getElementById('viewer').contentWindow.location.reload();
        attemptReload++;
        document.getElementById('process-status').innerText = `Reproducing issue: reloaded ${attemptReload} times`;
        if (attemptReload >= 10) {
            clearInterval(processId)
            document.getElementById('process-status').innerText = `Reproducing issue: reload DONE ${attemptReload} times`;
            unlockButton()
        };
    }, 2000);
}

document.getElementById('reloadIssue').addEventListener('click', reloadIssue);

// End

// Un-clock button
function unlockButton() {
    document.querySelectorAll('[data-lock-by-default]').forEach((item) => {
        item.removeAttribute('disabled');
    });
}

document.getElementById('release-by-image-id').addEventListener('click', (e) => {
    const crrImgId = document.getElementById('image-id').value;

    fetch(`/Home/ReleaseImageById?imageId=${crrImgId}`)
        .then(respone => respone.text())
        .then((text) => {
            document.getElementById('process-status').innerHTML = text;
        }).catch((err) => {
            document.getElementById('process-status').innerText = `${err}`;
        })
})

document.getElementById('release-by-sessions').addEventListener('click', (e) => {
    fetch(`/Home/ReleaseImageBySessionId`)
        .then(respone => respone.text())
        .then((text) => {
            document.getElementById('process-status').innerHTML = text;
        }).catch((err) => {
            document.getElementById('process-status').innerText = `${err}`;
        })
})

document.getElementById('release-all').addEventListener('click', (e) => {
    fetch(`/Home/ReleaseAllImages`)
        .then(respone => respone.text())
        .then((text) => {
            document.getElementById('process-status').innerHTML = text;
        }).catch((err) => {
            document.getElementById('process-status').innerText = `${err}`;
        })
})

document.getElementById('check-sessions').addEventListener('click', (e) => {
    fetch(`/Home/GetSessionCount`)
        .then(respone => respone.text())
        .then((text) => {
            document.getElementById('process-status').innerHTML = text;
        }).catch((err) => {
            document.getElementById('process-status').innerText = `${err}`;
        })
})