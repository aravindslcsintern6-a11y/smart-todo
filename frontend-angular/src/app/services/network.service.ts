//This is the API caller

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface NetworkScan {
  id: number;
  target: string;
  scanResult: string;
  scannedAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class NetworkService {

  private apiUrl = 'http://172.17.5.120:5292/api/network'; 
  // change port number if backend is different

  constructor(private http: HttpClient) {}

  scan(target: string): Observable<NetworkScan> {
    return this.http.post<NetworkScan>(`${this.apiUrl}/scan`, { target });
  }

  getHistory(): Observable<NetworkScan[]> {
    return this.http.get<NetworkScan[]>(`${this.apiUrl}/history`);
  }
}