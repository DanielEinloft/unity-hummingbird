# Unity MLAgents - Hummingbird

This project is a study on Unity's MLAgents Toolkit based on the online course ML-Agents: Hummingbirds available on Unity Learn page [link here]. Taught by Adam Kelly, the course gives a great starting point to learn MLAgents, so if you're interested please check it out here[link].

The following requirements are based on the current version of this project. To use the latest version of MLAgents, please check out the official documentation to address any issues regarding file formats and other incompatibilities.
#
### Requirements
* Unity (2018.4 or later)
* Python (3.6.1 or later)
* MLAgents Package (1.0.7)

### Requirements to train a neural network
* Anaconda (1.7.2 or later) [https://www.anaconda.com/products/individual#Downloads]
* Pytorch (1.7.1 or later)
* Python mlagents (0.20.0)
#
## Getting Started

    # Get the latest snapshot
    git clone --depth=1 https://github.com/DanielEinloft/unity-hummingbird.git

    # Install Python (https://www.python.org/downloads/)
    # Install Unity (https://unity3d.com/get-unity/download)
    # Open the project on Unity Hub
    # With the project opened, click on Window>Asset Manager and search for ML Agents. Click on 'See other versions' and download v1.0.7.
    # Click 'Play'
#
## Neural Network training steps

    # Install Anaconda (https://www.anaconda.com/products/individual#Downloads)
    # Open Anaconda prompt create an environment
    conda create -n ml-agents-01 python=3.7

    # Enter the environment and install the python packages needed (Pytorch and MLAgents)
    conda activate ml-agents-1.0

    pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html

    pip install mlagents==0.20.0

    # Use the trainer_config.yalm file available on the next section to configure the neural network/training information
    # On Anaconda, move to the location of the trainer_config.yalm file and check if everything is running as expected
    mlagents-learn -h

    # If everything is working, start the python neural network trainer
    mlagents-learn ./trainer_config.yaml --run-id hb_01

    # Finally, click Play on Unity (make sure the hummingbird game object doesn't have a neural network attached on Model property on Behavior Parameter component)

#
## trainer_config.yaml example


    behaviors:
        Hummingbird:
            trainer_type: ppo
            hyperparameters:
                batch_size: 2048
                buffer_size: 20480
                learning_rate: 3.0e-4
                learning_rate_schedule: linear
                beta: 5.0e-3
                epsilon: 0.2
                lambd: 0.95
                num_epoch: 3
            network_settings:
                vis_encode_type: simple
                normalize: false
                hidden_units: 256
                num_layers: 2
                memory:
                    memory_size: 128
                    sequence_length: 64
            max_steps: 5.0e6
            time_horizon: 128
            summary_freq: 10000
            reward_signals:
                extrinsic:
                    strength: 1.0
                    gamma: 0.99