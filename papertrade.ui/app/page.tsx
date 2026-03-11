// frontend/pages/index.tsx
"use client";
import { useEffect, useState } from 'react';
import { fetchFromApi } from '../services/api';

// Define the type for the data expected from the API
interface ApiResponse {
  id: number;
  name: string;
  // Add other properties as needed
}

export default function Home() {
  const [data, setData] = useState<ApiResponse[] | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Replace 'your-endpoint' with your actual endpoint, like 'users'
        const result = await fetchFromApi<ApiResponse[]>('image');
        setData(result);
      } catch (err) {
        setError((err as Error).message);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <h1>API Test</h1>
      {error && <p>Error: {error}</p>}
      {data ? (
        <pre>{JSON.stringify(data, null, 2)}</pre>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}
