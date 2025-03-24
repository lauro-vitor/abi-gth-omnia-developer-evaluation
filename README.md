# ABI GTH Omnia Developer Evaluation

This project is a .NET-based API that can be run using Docker.

## Getting Started

Follow the steps below to clone and run the project.

### Prerequisites

- [Git](https://git-scm.com/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Installation and Execution

1. **Clone the repository**

   ```sh
   git clone https://github.com/lauro-vitor/abi-gth-omnia-developer-evaluation.git
   ```

2. **Navigate to the project directory**

   ```sh
   cd abi-gth-omnia-developer-evaluation
   ```

3. **Build the API image**

   ```sh
   docker build -t ambevdeveloperevaluationwebapi -f src/Ambev.DeveloperEvaluation.WebApi/Dockerfile .
   ```

4. **Create and start the containers**

   ```sh
   docker compose up -d
   ```

5. **Access the API**

   Open your browser and go to:

   ```
   http://localhost:8080
   ```


