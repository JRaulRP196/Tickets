import { tokenStorage } from "./TokenStorage";

export class ApiError extends Error {
  status: number;

  constructor(status: number, message: string) {
    super(message);
    this.status = status;
    this.name = "ApiError";
  }
}

export async function request<T>(
  url: string,
  body?: unknown,
  method: "GET" | "POST" | "PUT" | "DELETE" | "PATCH" = "GET",
): Promise<T> {
  const token = tokenStorage.get();

  const res = await fetch(url, {
    method,
    headers: {
      ...(body !== undefined && { "Content-Type": "application/json" }),
      ...(token && { Authorization: `Bearer ${token}` }),
    },
    body: body !== undefined ? JSON.stringify(body) : undefined,
  });

  if (!res.ok) {
    if (res.status === 401) window.dispatchEvent(new Event("sesion-expirada"));
    throw new ApiError(
      res.status,
      (await res.text()) || `Error : ${res.status}`,
    );
  }
  if (res.status === 204) return null as T;
  return (await res.json()) as T;
}
