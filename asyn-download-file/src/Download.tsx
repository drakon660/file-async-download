export const Download: React.FC = () => {
    const handleDownload = async () => {
        try {
            const response = await fetch('https://localhost:7079/download/download-stream');
            if (!response.ok) {
                throw new Error(`Error: ${response.statusText}`);
            }
            const reader = response!.body?.getReader();
            let receivedLength = 0; 
            let chunks = []; 
            
            while (true) {
                const { done, value } = await reader!.read();

                if (done) {
                    break;
                }

                chunks.push(value);
                receivedLength += value.length;

                console.log(`Received ${receivedLength} bytes of data so far`);
            }

            // Combine chunks into a single Uint8Array
            let chunksAll = new Uint8Array(receivedLength);
            let position = 0;
            for(let chunk of chunks) {
                chunksAll.set(chunk, position); 
                position += chunk.length;
            }

            const blob = new Blob([chunksAll]);
            const url = window.URL.createObjectURL(blob);

            const a = document.createElement('a');
            a.href = url;
            a.download = 'downloadedData.txt'; // The filename for the download
            document.body.appendChild(a);
            a.click();

            // Clean up by removing the element and revoking the URL
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);

        } catch (error) {
            console.error('Download failed', error);
        }
    };

    return (
        <div>
            <button onClick={handleDownload}>Download File</button>
        </div>
    );
};
