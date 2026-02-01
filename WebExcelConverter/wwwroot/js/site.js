// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let selectedFormat = 'json';

function selectFormat(format) {
    selectedFormat = format;
    const jsonBtn = document.getElementById('jsonBtn');
    const xmlBtn = document.getElementById('xmlBtn');
    const outputFormat = document.getElementById('outputFormat');
    const convertBtn = document.getElementById('convertBtn');
    outputFormat.value = format;

    if (format === 'json') {
        jsonBtn.classList.remove('btn-primary');
        jsonBtn.classList.add('btn-success');
        xmlBtn.classList.remove('btn-success');
        xmlBtn.classList.add('btn-secondary');
        convertBtn.textContent = 'Spustit konverzi do JSON';
    } else {
        xmlBtn.classList.remove('btn-primary');
        xmlBtn.classList.add('btn-success');
        jsonBtn.classList.remove('btn-success');
        jsonBtn.classList.add('btn-secondary');
        convertBtn.textContent = 'Spustit konverzi do XML';
    }
}
document.addEventListener('DOMContentLoaded', function () {
    const downloadLink = document.querySelector('a[href*="DownloadFile"]');
    if (downloadLink) {
        const isXml = downloadLink.textContent.includes('XML');
        selectFormat(isXml ? 'xml' : 'json');
    } else {
        selectFormat('json');
    }
});