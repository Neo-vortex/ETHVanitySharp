# ETH Vanity Address Generator

Generate Ethereum addresses with a custom prefix and/or postfix using the Secp256k1 elliptic curve address generator.

## Features

- Create Ethereum addresses with a specified prefix and/or postfix.
- Multi-threaded implementation for improved performance.
- Real-time progress updates and statistics display.

## Getting Started

### Prerequisites

- .NET SDK (version 8.0 or higher)
- Linux Machine (Windows and Mac support is a TODO)

### Installation

```bash
git clone https://github.com/yourusername/ETHVanitySharp.git
cd ETHVanitySharp
dotnet build
dotnet run
```

### Usage
- Enter the desired prefix and postfix when prompted.
- The application will start generating Ethereum addresses in the background.
- Real-time progress updates will be displayed, including the generated address and private key when a matching address is found.

  
### Performance Comparison

| Platform                        | Threads | Speed (Addresses/Second) | Performance Ratio vs Competitor |
|---------------------------------|---------|---------------------------|--------------------------------|
| ETH Vanity Address Generator    | 20      | 470,000                   | 18.1x                          |
| [Competitor: vanity-eth.tk](https://vanity-eth.tk/) | 20      | 26,000                    | 1x                             |

**Performance Gain:**
- The ETH Vanity Address Generator outperforms [vanity-eth.tk](https://vanity-eth.tk/) by approximately 18.1 times in terms of address generation speed.

*Note: The performance comparison is based on testing on an Intel Core i7 12700 with 32GB of DDR4 memory running Ubuntu 22.04.*
