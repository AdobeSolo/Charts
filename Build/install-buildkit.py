__author__ = 'RoyCheng'
import os
import subprocess
import shutil

build_dir = os.path.abspath(os.path.dirname(__file__))
nuget_exe = os.path.join(build_dir, 'nuget.exe')

tools_dir = os.path.join(build_dir, 'tools')
if os.path.isdir(tools_dir):
    try:
        shutil.rmtree(tools_dir)
    except PermissionError:
        pass

__temp_dir = os.path.join(build_dir, '_temp_')
subprocess.check_call('\"%s\" install BuildKit -OutputDirectory \"%s\"' % (nuget_exe, __temp_dir))

__buildkit_dir_name = [dir for dir in os.listdir(__temp_dir) if dir.lower().startswith('buildkit')][0]
try:
    shutil.copytree(os.path.join(__temp_dir, __buildkit_dir_name, 'tools'), tools_dir)
except FileExistsError:
    pass
shutil.rmtree(__temp_dir)
