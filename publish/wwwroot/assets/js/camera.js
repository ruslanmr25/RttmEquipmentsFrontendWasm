window.cameraFunctions = {
    startCamera: async function (videoId, facingMode) {
        const video = document.getElementById(videoId);
        if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia) {
            alert("Sizning brauzeringiz kamera ishlashini qo'llab-quvvatlamaydi.");
            return;
        }

        if (video.srcObject) {
            // Avvalgi streamni to‘xtatish
            video.srcObject.getTracks().forEach(track => track.stop());
        }

        try {
            const stream = await navigator.mediaDevices.getUserMedia({
                video: { facingMode: facingMode || "user" }
            });
            video.srcObject = stream;
            await video.play();
        } catch (err) {
            alert("Kamera ochilmadi: " + err.message);
        }
    },

    takePhoto: function (videoId, canvasId, imgId) {
        const video = document.getElementById(videoId);
        const canvas = document.getElementById(canvasId);
        const img = document.getElementById(imgId);

        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        const context = canvas.getContext('2d');
        context.drawImage(video, 0, 0, canvas.width, canvas.height);

        const dataUrl = canvas.toDataURL('image/png');
        img.src = dataUrl;

        return dataUrl;
    },

    stopCamera: function (videoId) {
        const video = document.getElementById(videoId);
        if (video && video.srcObject) {
            video.srcObject.getTracks().forEach(track => track.stop());
            video.srcObject = null;
        }
    }
};


window.modalHelper = {
    show: (id) => {
        var modal = new bootstrap.Modal(document.getElementById(id));
        modal.show();
    },
    hide: (id) => {
        var modal = bootstrap.Modal.getInstance(document.getElementById(id));
        if (modal) modal.hide();
    }
};
