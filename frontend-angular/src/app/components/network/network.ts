import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NetworkService, NetworkScan } from '../../services/network.service';

@Component({
  selector: 'app-network',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './network.html',
  styleUrls: ['./network.css']
})
export class Network {

  target: string = '';
  result: string = '';
  history: NetworkScan[] = [];

  constructor(private networkService: NetworkService) {}

  scan() {
  if (!this.target.trim()) return;

  console.log("Sending target:", this.target);

  this.networkService.scan(this.target).subscribe({
    next: (res) => {
      console.log("Response:", res);
      this.result = res.scanResult;
      this.loadHistory();
    },
    error: (err) => {
      console.error("Error:", err);
    }
  });
}

loadHistory() {
    this.networkService.getHistory().subscribe({
      next: (data) => {
        this.history = data;
      },
      error: (err) => {
        console.error("History error:", err);
      }
    });
  }

  ngOnInit() {
    this.loadHistory();
  }
}