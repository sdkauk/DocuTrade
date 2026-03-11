// frontend/services/api.ts

export async function fetchFromApi<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
  const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`API request failed with status ${response.status}`);
  }

  return response.json() as Promise<T>;
}
